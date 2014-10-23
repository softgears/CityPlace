//
//  PropDescriptionViewController.m
//  CityPlace
//
//  Created by Yuri Korshev on 23.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "PropDescriptionViewController.h"

@interface PropDescriptionViewController ()

@end

@implementation PropDescriptionViewController
@synthesize propFullText;
@synthesize propText;

- (void)viewDidLoad {
    [super viewDidLoad];
    
    UIBarButtonItem *item = [[UIBarButtonItem alloc] initWithImage:[UIImage imageNamed:@"back"] style:UIBarButtonItemStylePlain target:self action:@selector(goBack)];
    item.tintColor = [UIColor whiteColor];
    self.navigationItem.leftBarButtonItem = item;
    
    propFullText.text = propText;
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void) goBack {
    [self.navigationController popViewControllerAnimated:YES];
}

/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

@end
